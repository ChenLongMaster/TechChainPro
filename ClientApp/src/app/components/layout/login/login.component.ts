import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { MessageService } from 'primeng/api';
import { AuthenticateModel, LoginModel } from 'src/app/model/user.model';
import { StorageQuerySerive } from 'src/app/service/core/storage.query.service';
import { UserService } from 'src/app/service/user.service';

@UntilDestroy()
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({});

  constructor(private userService: UserService,
              private storageQueryService: StorageQuerySerive,
              private messageService: MessageService) { }
  
  @Output() displayChange = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.loginForm = new FormGroup({ 
      username: new FormControl(''),
      password: new FormControl(''),
    });

  }

  onSubmit(){
    this.userService.AuthenticateUser(this.loginForm.value as LoginModel).pipe(untilDestroyed(this)).subscribe((result:any) => {
      if(result.token){
        this.messageService.add({severity:'success', summary: 'Welcome!', detail: 'Login Successfully',sticky :true});
        this.storageQueryService.SetToken(result.token);
        this.storageQueryService.SetUsername(result.username);
        this.close();
      }
      else {
        this.messageService.add({severity:'error', summary: 'Error', detail: 'Login Failed',sticky :true});
      }

    });
  }

  close(){
    this.displayChange.emit(false);
  }


}
