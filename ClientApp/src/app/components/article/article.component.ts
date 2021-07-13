import { Component, OnInit } from '@angular/core';
import { ArticleModel } from 'src/app/model/article.model';
import { ArticleService } from 'src/app/service/article.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})


export class ArticleComponent implements OnInit {

  constructor(private articleService : ArticleService) { }


  articleName: string = ''; 
  articleContent: string = '';

  ngOnInit(): void {
    this.articleContent = '';
    this.GetArticleById('EFD1B83D-C069-4630-A2AA-C47B018662C1');
  }

  GetArticleById(id: string){
    this.articleService.GetArticleById(id).pipe().subscribe((data : any) => {
      this.articleName = data.name;
      this.articleContent = data.displayContent;
    })
  }
}
