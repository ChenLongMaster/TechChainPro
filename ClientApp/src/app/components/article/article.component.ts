import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})


export class ArticleComponent implements OnInit {

  constructor(private userService : UserService) { }

  userName: string = ''; 
  articleContent: string = '';

  ngOnInit(): void {
    this.articleContent = '';
    // this.getUserName('EF9F8097-88F6-4274-83BF-544D47EDE98C');
  }

  getUserName(id: string){
    this.userService.GetUserById(id).pipe().subscribe((data : any) => {
      this.userName = data.userName;
    })
  }
}
