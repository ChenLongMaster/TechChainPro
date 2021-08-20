import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { ArticleDetailComponent } from './components/article/article-detail/article-detail.component';
import { ArticleEditorComponent } from './components/article/article-editor/article-editor.component';
import { ArticleListComponent } from './components/article/article-list/article-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/articles', pathMatch: 'full' },
  {
    path: 'articles',
    pathMatch: 'full',
    component: ArticleListComponent,
  },
  { path: 'articles/:category/:id/:slug', component: ArticleDetailComponent },
  { path: 'articles/editor/create', component: ArticleEditorComponent },
  { path: 'articles/editor/:category/:id/:slug', component: ArticleEditorComponent },
  { path: 'about', component: AboutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})

export class AppRoutingModule { }
