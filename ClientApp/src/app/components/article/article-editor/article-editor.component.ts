import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ArticleModel } from 'src/app/model/article.model';
import { CategoryModel } from 'src/app/model/category.model';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';
import { CommonService } from 'src/app/service/common.service';
import { Location } from '@angular/common';
import UploadAdapter from 'src/app/service/core/upload-adapter.service';
import { AuthorizationService } from 'src/app/service/authorization.service';
import { AutheticationService } from 'src/app/service/authentication.service';

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
  outputValue: string = "";

  canDelete: boolean;
  headerTitle: string = "New Article";

  get formName() {
    return this.editorFormGroup.controls['name'];
  }

  get formAbstract() {
    return this.editorFormGroup.controls['abstract'];
  }

  get formCategory() {
    return this.editorFormGroup.controls['category'];

  }

  constructor(
    private articleService: ArticleService,
    private autheticationService: AutheticationService,
    private authorizationService: AuthorizationService,
    private commonService: CommonService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private activeRoute: ActivatedRoute,
    private router: Router,
    private location: Location) { }


  public onReady(editor: any) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );

    editor.plugins.get('FileRepository').createUploadAdapter = (loader: any) => {
      return new UploadAdapter(loader);
    };
  }

  ngOnInit(): void {
    this.viewModel.displayContent = !this.viewModel.displayContent ? '' : this.viewModel.displayContent; //CK editor null error
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
      this.headerTitle = "Edit Article";
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

      if (this.id) {
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
      else {
        const currentUser = this.autheticationService.GetDecodedTokenDetail();
        if (!currentUser) {
          this.autheticationService.triggerLogin.next(true);
          return;
        }
        this.articleService.CreateArticle(this.viewModel).pipe(untilDestroyed(this)).subscribe((result: boolean) => {
          if (result) {
            this.messageService.add({ severity: 'success', summary: 'Create Successful', detail: `Article has been created Successfully.` });
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
    this.articleService.GetArticleById(this.id).pipe(untilDestroyed(this)).subscribe((returneData: ArticleModel) => {
      this.viewModel = returneData;
      this.initRepresentImage = true;
      this.editorFormGroup.patchValue(this.viewModel);
      this.editorFormGroup.controls['category'].setValue(new OptionObject(this.viewModel.categoryName, this.viewModel.categoryId,));
      this.canDelete = this.authorizationService.CheckDeleteArticlePermisson(this.viewModel.authorId);
    });
  }

  Delete() {
    this.confirmationService.confirm({
      message: 'Are you sure want to delete this article?',
      header: 'Delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.articleService.DeleteArticle(this.viewModel.id).pipe(untilDestroyed(this)).subscribe((result) => {
          if (result) {
            this.messageService.add({ severity: 'success', summary: 'Delete Successful', detail: `Article has been deleted.` });
            this.router.navigateByUrl(`/articles`);
          }
        });
      },
      reject: () => {
      }
    });
  }

  initCategoryItems() {
    this.commonService.GetCategoryItem().pipe(untilDestroyed(this)).subscribe((returnData: CategoryModel[]) => {
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

  goToPreviousPage() {
    this.location.back();
  }
}
