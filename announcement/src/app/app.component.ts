import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnnouncementCardComponent } from './common-ui/announcement-card/announcement-card.component';
import { AnnouncementService } from './data/services/announcement.service';
import { CommonModule } from '@angular/common';
import { Announcement } from './data/interfaces/announcement.interface';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AnnouncementCardComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  
}
