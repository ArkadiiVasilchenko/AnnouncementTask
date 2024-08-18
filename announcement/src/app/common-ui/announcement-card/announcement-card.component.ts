import { Component, inject, Input } from '@angular/core';
import { Announcement } from '../../data/interfaces/announcement.interface';

@Component({
  selector: 'app-announcement-card',
  standalone: true,
  imports: [],
  templateUrl: './announcement-card.component.html',
  styleUrls: ['./announcement-card.component.scss']
})
export class AnnouncementCardComponent {
  @Input() announcement!: Announcement;
}
