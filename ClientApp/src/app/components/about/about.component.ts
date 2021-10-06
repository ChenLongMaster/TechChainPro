import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Constants } from 'src/app/constants';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
  title: string = `${Constants.AppName} - About`;
  constructor(private titleService: Title
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle(this.title);
  }

}
