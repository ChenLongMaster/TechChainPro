import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { MessageService } from 'primeng/api';
import { ArticleModel } from 'src/app/model/article.model';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';
import { UploadService } from 'src/app/service/upload.service';
import UploadAdapterService from 'src/app/service/upload-adapter.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
  representImageSelected: boolean;
  imageFile: File;
  categoryOptions: OptionObject[];

  isShowOutput:boolean = false;
  
  get formName() {
    return this.editorFormGroup.controls['name'];
  }

  get formAbstract() {
    return this.editorFormGroup.controls['abstract'];
  }

  get formCategory() {
    return this.editorFormGroup.controls['category'];

  }
  get formDisplayContent() {
    return this.editorFormGroup.controls['displayContent'];
  }

  public onChange({ editor }: ChangeEvent) {
    console.info(editor.getData());
    this.editorFormGroup.controls['displayContent'].patchValue(editor.getData());
  }

  constructor(
    private articleService: ArticleService,
    private uploadService: UploadService,
    private messageService: MessageService) { }

  public onReady(editor: any) {
    this.categoryOptions = this.articleService.InitCategoryItems();

    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );

    editor.plugins.get('FileRepository').createUploadAdapter = (loader: any) => {
      return new UploadAdapterService(loader);
    };
  }

  ngOnInit(): void {
    this.headerName = 'New Article'
    this.editorFormGroup = new FormGroup({
      'name': new FormControl('', Validators.required),
      'category': new FormControl('', Validators.required),
      'abstract': new FormControl(),
      'displayContent': new FormControl(),
    });
  }

  saveArticle() {
    this.editorFormGroup.markAllAsTouched();
    if (this.editorFormGroup.status == "VALID") {
      this.viewModel.name = this.formName.value;
      this.viewModel.categoryId = this.formCategory.value.value;
      this.viewModel.abstract = this.formAbstract.value;
      this.viewModel.displayContent = this.formDisplayContent.value;

      this.articleService.CreateArticle(this.viewModel).pipe(untilDestroyed(this)).subscribe((result: boolean) => {
        if (result) {
          this.messageService.add({ severity: 'success', summary: 'Create Sucessfull', detail: `Article has been created sucessfully.`, sticky: true });
        }
        else {
          this.messageService.add({ severity: 'error', summary: 'Create Failed', detail: 'Cannot Save The Article.', sticky: true });
        }
      })
    }
  }

  onImageSelect(event: any) {
    this.representImageSelected = true;
    this.imageFile = event.files[0];

    if (event.files.length != 0) {
      this.representImageSelected = true;
    }

    this.uploadService.UploadImage(event.files[0]).pipe().subscribe((resonse: any) => {
      this.viewModel.representImageUrl = resonse.uploadedUrl;
      this.messageService.add({ severity: 'success', summary: 'Sucess', detail: "Image Uploaded Sucessfully." });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: "Image failed to upload.", sticky: true });
      });
  }

  onClear(event: any) {
    if (event?.file === undefined) {
      this.representImageSelected = false;
    }
  }

  ToogleOutput(){
    this.isShowOutput = !this.isShowOutput;
  }
}
