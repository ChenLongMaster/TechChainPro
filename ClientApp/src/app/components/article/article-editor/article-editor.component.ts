import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document'; 

@Component({
  selector: 'app-article-editor',
  templateUrl: './article-editor.component.html',
  styleUrls: ['./article-editor.component.scss']
})
export class ArticleEditorComponent implements OnInit {
  public Editor = DecoupledEditor;
  @Input() ArticleId: string;

  editorFormGroup: FormGroup;
  headerName : String;
  outputContent: string;
  
  constructor() { }

  public onReady( editor:any ) {
    editor.ui.getEditableElement().parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
    );
}

  ngOnInit(): void {
    
    this.headerName = 'New Article'
    this.editorFormGroup = new FormGroup({ 
      'title' : new FormControl(),
      'editorContent' : new FormControl()
    });
  }

  saveArticle() {

  }

  get outputContentValue(){
    return this.editorFormGroup.controls['editorContent'].value;
  }

}
