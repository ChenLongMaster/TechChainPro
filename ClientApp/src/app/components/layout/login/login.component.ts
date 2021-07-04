import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
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
              private storageQueryService: StorageQuerySerive) { }
  
  @Output() displayChange = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.loginForm = new FormGroup({ 
      username: new FormControl(''),
      password: new FormControl(''),
    });

  }

  onSubmit(){
    this.userService.AuthenticateUser(this.loginForm.value as LoginModel).pipe(untilDestroyed(this)).subscribe((result:any) => {
      this.storageQueryService.SetToken(result.token);
      this.storageQueryService.SetUsername(result.username);
    });
  }

  close(){
    this.displayChange.emit(false);
  }


}
