import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { ArticleModel } from 'src/app/model/article.model';
import { ArticleService } from 'src/app/service/article.service';
import { untilDestroyed } from '@ngneat/until-destroy';
import { MessageService } from 'primeng/api';
import { OptionObject } from 'src/app/model/optionObject.model';
import { CategoryEnum } from 'src/app/service/core/category.enum';

@Component({
  selector: 'app-article-editor',
  templateUrl: './article-editor.component.html',
  styleUrls: ['./article-editor.component.scss']
})
export class ArticleEditorComponent implements OnInit {
  public Editor = DecoupledEditor;
  @Input() ArticleId: string;

  viewModel: ArticleModel = new ArticleModel();
  editorFormGroup: FormGroup;
  headerName: String;
  outputContent: string;
  representImage: any[] = [];
  imageSelected: boolean;
  imageUrl: string;
  categoryOptions: OptionObject[];

  get formName() {
    return this.editorFormGroup.controls['name'];

  }

  get formCategory() {
    return this.editorFormGroup.controls['category'];

  }
  get formDisplayContent() {
    return this.editorFormGroup.controls['displayContent'];
  }

  constructor(private articleService: ArticleService,
    private messageService: MessageService) { }

  public onReady(editor: any) {
    this.categoryOptions = [
      { name: CategoryEnum[CategoryEnum.Blockchain], code: CategoryEnum.Blockchain },
      { name: CategoryEnum[CategoryEnum.DotNet], code: CategoryEnum.DotNet },
      { name: CategoryEnum[CategoryEnum.Blockchain], code: CategoryEnum.Blockchain },
      { name: CategoryEnum[CategoryEnum.Blockchain], code: CategoryEnum.Blockchain },
    ];
    console.log(this.categoryOptions);
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  ngOnInit(): void {
    this.headerName = 'New Article'
    this.editorFormGroup = new FormGroup({
      'name': new FormControl('', Validators.required),
      'abstract': new FormControl('', Validators.required),
      'displayContent': new FormControl(),
      'category': new FormControl('', Validators.required)
    });
  }

  saveArticle() {
    this.viewModel = Object.assign(this.viewModel, this.editorFormGroup.getRawValue());
    this.editorFormGroup.markAllAsTouched();
    if (this.editorFormGroup.status == "VALID") {
      this.articleService.CreateArticle(this.viewModel).pipe().subscribe((result: boolean) => {
        if (result) {
          this.messageService.add({ severity: 'success', summary: 'Create Sucessfull', detail: `Article "${this.viewModel.name}" has been created sucessfully.`, sticky: true });
        }
        else {
          this.messageService.add({ severity: 'error', summary: 'Access Denied', detail: 'You Are Unauthorized.', sticky: true });
        }
      })
    }
  }

  onUpload(event: any) {
    for (let file of event.files) {
      this.representImage.push(file);
    }

    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }
  onImageSelect(event: any) {
    this.imageSelected = true;
    for (let i = 0; i < event.currentFiles.length; i++) {
      var item = event.currentFiles[i];
      this.imageUrl = item.objectURL.changingThisBreaksApplicationSecurity;
    }
    // if (event.files.length != 0) {
    //   this.imageSelected = true;
    // }
  }
  onClear(event: any) {
    if (!event.file === undefined) {
      this.imageSelected = false;
    }
  }
}
