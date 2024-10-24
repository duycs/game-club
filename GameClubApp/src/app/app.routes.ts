import { Routes } from '@angular/router';
import { ClubsComponent } from './modules/clubs/clubs.component';

export const appRoutes: Routes = [
    { path: '', redirectTo: '/clubs', pathMatch: 'full' },
    { path: '**', redirectTo: '/clubs', pathMatch: 'full'},
    { path: 'clubs', loadChildren: () => import('./modules/clubs/clubs.module').then(m => m.ClubsModule), component: ClubsComponent},
];
