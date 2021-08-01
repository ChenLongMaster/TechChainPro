import { AfterViewInit, ChangeDetectorRef, Component, Input, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { MessageService } from 'primeng/api';
import { ArticleModel } from 'src/app/model/article.model';
import { CategoryModel } from 'src/app/model/category.model';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';
import { CommonService } from 'src/app/service/common.service';
import UploadAdapterService from 'src/app/service/upload-adapter.service';

@UntilDestroy()
@Component({
  selector: 'app-article-editor',
  templateUrl: './article-editor.component.html',
  styleUrls: ['./article-editor.component.scss']
})
export class ArticleEditorComponent implements OnInit {
  public Editor = DecoupledEditor;
  @Input() ArticleId: string;


  isNew: boolean;
  id: string | null;
  viewModel: ArticleModel = new ArticleModel();
  editorFormGroup: FormGroup;
  outputContent: string;
  representImageSelected: boolean;
  initRepresentImage: boolean;
  imageFile: File;
  categoryOptions: OptionObject[];

  isShowOutput: boolean = false;
  testcontent: string = "<p>test</p>";
  outputValue: string = "";
  get formName() {
    return this.editorFormGroup.controls['name'];
  }

  get formAbstract() {
    return this.editorFormGroup.controls['abstract'];
  }

  get formCategory() {
    return this.editorFormGroup.controls['category'];

  }

  // public onChange({ editor }: ChangeEvent) {
  //   console.info(editor.getData());
  //   this.editorFormGroup.controls['displayContent'].patchValue(editor.getData());
  // }

  constructor(
    private articleService: ArticleService,
    private commonService: CommonService,
    private messageService: MessageService,
    private activeRoute: ActivatedRoute,
    private detector: ChangeDetectorRef,
    private ngZone: NgZone,
    private router: Router) { }


  public onReady(editor: any) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );

    editor.plugins.get('FileRepository').createUploadAdapter = (loader: any) => {
      return new UploadAdapterService(loader);
    };
  }

  ngOnInit(): void {
    this.viewModel.displayContent = "<p>Test Content</p>";

    this.activeRoute.params.subscribe(params => {
      this.id = params['id'];
    });

    this.initCategoryItems();
    this.editorFormGroup = new FormGroup({
      'name': new FormControl('', Validators.required),
      'category': new FormControl('', Validators.required),
      'abstract': new FormControl(),
    });
    if (this.id) {
      this.GetArticleById();
      this.viewModel.displayContent = this.viewModel.displayContent;
    }
  }

  saveArticle() {
    this.editorFormGroup.markAllAsTouched();
    if (this.editorFormGroup.status == "VALID") {
      this.viewModel.name = this.formName.value;
      this.viewModel.categoryId = this.formCategory.value.value;
      this.viewModel.abstract = this.formAbstract.value;

      if(this.id){
        this.articleService.UpdateArticle(this.viewModel).pipe(untilDestroyed(this)).subscribe((result) => {
          if (result) {
            this.messageService.add({ severity: 'success', summary: 'Update Sucessfull', detail: `Article has been updated sucessfully.` });
            this.router.navigateByUrl(`/articles/detail/${this.viewModel.id}`);
          }
          else {
            this.messageService.add({ severity: 'error', summary: 'Create Failed', detail: 'Cannot Save The Article.', sticky: true, closable: true });
          }
        });
      }
      else{
        this.articleService.CreateArticle(this.viewModel).pipe(untilDestroyed(this)).subscribe((result: boolean) => {
          if (result) {
            this.messageService.add({ severity: 'success', summary: 'Create Sucessfull', detail: `Article has been created sucessfully.` });
            this.router.navigateByUrl(`/articles`);
          }
          else {
            this.messageService.add({ severity: 'error', summary: 'Create Failed', detail: 'Cannot Save The Article.', sticky: true, closable: true });
          }
        })
      }
    }
  }

  GetArticleById() {
    this.articleService.GetArticleById(this.id).pipe().subscribe((returneData: ArticleModel) => {
      this.initRepresentImage = true;
      this.viewModel = returneData;
      this.editorFormGroup.patchValue(this.viewModel);
      this.editorFormGroup.controls['category'].setValue(new OptionObject(this.viewModel.categoryName, this.viewModel.categoryId,));
      this.detector.detectChanges();
    },
      () => {
        this.ngZone.run(() => {
          this.viewModel = this.viewModel;
        });
      }
    );
  }

  initCategoryItems() {
    this.articleService.GetCategoryItem().pipe(untilDestroyed(this)).subscribe((returnData: CategoryModel[]) => {
      this.categoryOptions = returnData.filter(x => x.id != 1).map(x => new OptionObject(x.name, x.id));
    });
  }
  onImageSelect(event: any) {
    this.representImageSelected = true;
    this.imageFile = event.files[0];

    if (event.files.length != 0) {
      this.representImageSelected = true;
    }

    this.commonService.UploadImage(event.files[0]).pipe().subscribe((resonse: any) => {
      this.viewModel.representImageUrl = resonse.uploadedUrl;
      this.initRepresentImage = false;
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

  toogleOutput() {
    this.isShowOutput = !this.isShowOutput;
  }


}
