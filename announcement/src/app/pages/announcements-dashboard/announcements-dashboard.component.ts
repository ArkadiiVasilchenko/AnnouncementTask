import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnnouncementService } from '../../data/services/announcement.service';
import { Announcement } from '../../data/interfaces/announcement.interface';
import { AnnouncementCardComponent } from '../../common-ui/announcement-card/announcement-card.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-announcements-dashboard',
  standalone: true,
  imports: [RouterOutlet, AnnouncementCardComponent, CommonModule],
  templateUrl: './announcements-dashboard.component.html',
  styleUrl: './announcements-dashboard.component.scss'
})
export class AnnouncementsDashboardComponent {
  announcementService = inject(AnnouncementService)
  announcements : Announcement[] = []

  constructor(){
    this.announcementService.getAnnouncements().subscribe(value => {
      this.announcements = value
    })
  }
}
