import { Routes } from '@angular/router';
import { AnnouncementsDashboardComponent } from './pages/announcements-dashboard/announcements-dashboard.component';
import { AnnouncementCreationComponent } from './pages/announcement-creation/announcement-creation.component';

export const routes: Routes = [
    {path: '', component: AnnouncementsDashboardComponent},
    {path: 'creation', component: AnnouncementCreationComponent},
];
