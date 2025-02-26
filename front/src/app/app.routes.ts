import { Routes } from '@angular/router';
import { ApplicationListComponent } from './components/application/application-list/application-list.component';

export const routes: Routes = [
    {
        path: 'application-list',
        component: ApplicationListComponent
    },
    {
        path: '**',
        redirectTo: 'application-list'
    },
];
