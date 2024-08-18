import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AnnouncementService } from '../../data/services/announcement.service';

@Component({
  selector: 'app-announcement-creation',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './announcement-creation.component.html',
  styleUrl: './announcement-creation.component.scss'
})
export class AnnouncementCreationComponent {
  announcementService = inject(AnnouncementService)

  form = new FormGroup({
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required)
  });

  onSubmit(){
    if(this.form.valid){
      //@ts-ignore
      this.announcementService.createAnnouncement(this.form.value);
    }
  }
}
