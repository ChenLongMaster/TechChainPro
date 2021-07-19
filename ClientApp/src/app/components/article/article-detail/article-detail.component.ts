import { Component, OnInit } from '@angular/core';
import { ArticleService } from 'src/app/service/article.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss']
})
export class ArticleDetailComponent implements OnInit {


  constructor(private articleService : ArticleService) { }

  articleName: string = ''; 
  articleContent: string = '';

  ngOnInit(): void {
    this.articleContent = '';
    this.GetArticleById('1A9B79D4-8FF3-4B41-8A59-8F75AB9670FF');
  }

  GetArticleById(id: string){
    this.articleService.GetArticleById(id).pipe().subscribe((data : any) => {
      this.articleName = data.name;
      this.articleContent = data.displayContent;
    })
  }
}
