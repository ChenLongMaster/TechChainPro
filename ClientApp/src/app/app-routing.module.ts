import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleDetailComponent } from './components/article/article-detail/article-detail.component';
import { ArticleEditorComponent } from './components/article/article-editor/article-editor.component';
import { ArticleListComponent } from './components/article/article-list/article-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/articles/editor', pathMatch: 'full' },
  {
    path: 'articles',
    pathMatch: 'full',
    component: ArticleListComponent,
  },
  { path: 'articles/detail', component: ArticleDetailComponent, canActivate: [] },
  { path: 'articles/editor', component: ArticleEditorComponent, canActivate: [] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
