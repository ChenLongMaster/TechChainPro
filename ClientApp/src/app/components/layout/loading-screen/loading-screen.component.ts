import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-loading-screen',
  templateUrl: './loading-screen.component.html',
  styleUrls: ['./loading-screen.component.scss']
})
export class LoadingScreenComponent implements OnInit,OnChanges {
  @Input() loading = false;
  constructor() { }
  
  ngOnChanges(changes: SimpleChanges): void {
    if(changes.loading){
      this.loading = changes.loading.currentValue;
    }
  }

  ngOnInit(): void {
  }

}
