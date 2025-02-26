import { Routes } from '@angular/router';
import { ApplicationListComponent } from './components/application/application-list/application-list.component';
import { PasswordListComponent } from './components/password/password-list/password-list.component';

export const routes: Routes = [
    {
        path: 'application-list',
        component: ApplicationListComponent
    },
    {
        path: 'password-list',
        component: PasswordListComponent
    },
    {
        path: '**',
        redirectTo: 'application-list'
    },
];
