// app.routes.ts
import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { MainLayoutComponent } from './layout/main-layout.component';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./auth/pages/login-page/login-page.component')
      .then(m => m.LoginPage)
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/pages/dashboard-page/dashboard-page.component')
          .then(m => m.DashboardPage),
        canActivate: [AuthGuard]
      },
      {
        path: 'providers',
        children: [
          {
            path: '',
            loadComponent: () => import('./provider/pages/provider-page/providers-page.component')
              .then(m => m.ProviderPage)
          },
          {
            path: 'create',
            loadComponent: () => import('./provider/pages/create-provider/create-provider.component')
              .then(m => m.CreateProviderComponent)
          },
          {
            path: 'detail/:id',
            loadComponent: () => import('./provider/pages/detail-provider/detail-provider.component')
              .then(m => m.DetailProviderComponent)
          },
          {
            path: 'edit/:id',
            loadComponent: () => import('./provider/pages/edit-provider/edit-provider.component')
              .then(m => m.EditProviderComponent)
          }
        ],
        canActivate: [AuthGuard]
      },
      {
        path: 'catalogs',
        children: [
          {
            path: '',
            loadComponent: () => import('./catalog/pages/catalog-page/catalogs-page.component')
            .then(m => m.CatalogsPage),
          },
          {
            path: 'create',
            loadComponent: () => import('./catalog/pages/create-catalog/create-catalog.component')
              .then(m => m.CreateCatalogComponent)
          },
          {
            path: 'detail/:id',
            loadComponent: () => import('./catalog/pages/detail-catalog/detail-catalog.component')
              .then(m => m.DetailCatalogComponent)
          },
          {
            path: 'edit/:id',
            loadComponent: () => import('./catalog/pages/edit-catalog/edit-catalog.component')
              .then(m => m.EditCatalogComponent)
          }
        ],
        canActivate: [AuthGuard]
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
