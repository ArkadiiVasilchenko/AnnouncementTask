using Announcement.Application.Services.AnnouncementServices.AnnouncementServicesInterfaces;
using Announcement.Domain.Models;
using Announcement.Domain.Models.RequestDtos;
using Announcement.Persistence.Repositories.AnnouncementRepository;
using FluentValidation;

namespace Announcement.Application.Services.AnnouncementServices
{
    public class AnnouncementService : BaseService<AnnouncementEntity>, IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IValidator<CreateAnnouncementRequestDto> creationValidator;
        private readonly IValidator<UpdateAnnouncementRequestDto> updationValidator;

        public AnnouncementService(IAnnouncementRepository _announcementRepository, 
            IValidator<CreateAnnouncementRequestDto> _creationValidator,
            IValidator<UpdateAnnouncementRequestDto> _updationValidator) : base(_announcementRepository)
        {
            announcementRepository = _announcementRepository;
            creationValidator = _creationValidator;
            updationValidator = _updationValidator;
        }

        public async Task UpdateAsync(UpdateAnnouncementRequestDto requestDto)
        {
            var validationResult = await updationValidator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var announcementModel = await announcementRepository.ReadByIdAsync(requestDto.Id);

            if (announcementModel == null)
            {
                throw new NullReferenceException($"Announcement with ID {requestDto.Id} was not found.");
            }

            announcementModel.Title = requestDto.Title;
            announcementModel.Description = requestDto.Description;

            await announcementRepository.UpdateAsync(announcementModel);
        }

        public async Task СreateAsync(CreateAnnouncementRequestDto requestDto)
        {
            var validationResult = await creationValidator.ValidateAsync(requestDto);
            if (validationResult.IsValid)
            {
                AnnouncementEntity announcement = new AnnouncementEntity(requestDto.Title, requestDto.Description);
                await announcementRepository.CreateAsync(announcement);
            }
            else
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }
        }

        public async Task<List<AnnouncementEntity>> ReadByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Argument is invalid.");

            List<AnnouncementEntity> announcementEntitiesResult = new List<AnnouncementEntity>();
            Dictionary<int, int> similarAnnouncements = new Dictionary<int, int>();

            var announcement = await announcementRepository.ReadByIdAsync(id);

            if (announcement != null)
            {
                announcementEntitiesResult.Add(announcement);

                similarAnnouncements = GetTopSimilarAnnouncements(announcement);

                if (similarAnnouncements != null && similarAnnouncements.Count > 0)
                {
                    foreach (var similarAnnouncement in similarAnnouncements)
                    {
                        announcement = await announcementRepository.ReadByIdAsync(similarAnnouncement.Key);
                        announcementEntitiesResult.Add(announcement);
                    }
                }
            }

            return announcementEntitiesResult;
        }

        private Dictionary<int, int> GetTopSimilarAnnouncements(AnnouncementEntity targetAnnouncement)
        {
            var announcements =  announcementRepository.Read();

            var titleWordsDict = ExtractWordsFromAnnouncements(announcements, a => a.Title);
            var descriptionWordsDict = ExtractWordsFromAnnouncements(announcements, a => a.Description);

            var targetTitleWords = GetWordsFromText(targetAnnouncement.Title);
            var targetDescriptionWords = GetWordsFromText(targetAnnouncement.Description);

            var titleSimilarityScores = CalculateSimilarityScores(targetAnnouncement.Id, targetTitleWords, titleWordsDict);
            var descriptionSimilarityScores = CalculateSimilarityScores(targetAnnouncement.Id, targetDescriptionWords, descriptionWordsDict);

            var combinedScores = CombineScores(titleSimilarityScores, descriptionSimilarityScores);

            return combinedScores
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private Dictionary<int, List<string>> ExtractWordsFromAnnouncements(IEnumerable<AnnouncementEntity> announcements, Func<AnnouncementEntity, string> selector)
        {
            return announcements.ToDictionary(
                a => a.Id,
                a => GetWordsFromText(selector(a))
            );
        }

        private List<string> GetWordsFromText(string text)
        {
            return text
                .Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLowerInvariant())
                .ToList();
        }

        private Dictionary<int, int> CalculateSimilarityScores(int targetAnnouncementId, List<string> targetWords, Dictionary<int, List<string>> announcementWordsDictionary)
        {
            var similarityScores = new Dictionary<int, int>();

            foreach (var kvp in announcementWordsDictionary)
            {
                var announcementId = kvp.Key;
                var announcementWords = kvp.Value;

                if (targetAnnouncementId == announcementId) continue;

                var matchingWordsCount = targetWords.Intersect(announcementWords).Count();
                if (matchingWordsCount > 0)
                {
                    similarityScores[announcementId] = matchingWordsCount;
                }
            }

            return similarityScores;
        }

        private Dictionary<int, int> CombineScores(Dictionary<int, int> titleScores, Dictionary<int, int> descriptionScores)
        {
            var combinedScores = new Dictionary<int, int>();

            foreach (var titleScore in titleScores)
            {
                if (descriptionScores.TryGetValue(titleScore.Key, out var descriptionScore))
                {
                    combinedScores[titleScore.Key] = titleScore.Value + descriptionScore;
                }
                else
                {
                    combinedScores[titleScore.Key] = titleScore.Value;
                }
            }

            foreach (var descriptionScore in descriptionScores)
            {
                if (!combinedScores.ContainsKey(descriptionScore.Key))
                {
                    combinedScores[descriptionScore.Key] = descriptionScore.Value;
                }
            }

            return combinedScores;
        }
    }
}
