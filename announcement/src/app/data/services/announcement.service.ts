import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Announcement } from '../interfaces/announcement.interface';


@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {
  http = inject(HttpClient)

  constructor() { }

  getAnnouncements() {
    return this.http.get<Announcement[]>("https://localhost:44360/api/Announcement/GetAnnouncements");
  }

  createAnnouncement(payload: { title: string; description: string }){
    return this.http.post<Announcement>("https://localhost:44360/api/Announcement/CreateAnnouncement", payload);
  }
}
