import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Result } from './models/result';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  serverUri = 'https://localhost:44386/api/test/';

  headers: HttpHeaders = new HttpHeaders();

  publicResponse: Result;
  privateResponse: Result;
  permissionResponse: Result;

  constructor(private httpClient: HttpClient){
    this.headers.set('Content-Type', 'application/json');
    this.headers.set('Accept', 'application/json');
  }

  onPublicButtonClick(): void{
    console.log('response', this.publicResponse);
    this.httpClient.get<Result>(this.serverUri + 'public', {headers: this.headers}).subscribe(
      result => this.publicResponse = result,
      err => this.publicResponse  = this.isUnauthorized(err)
    );
  }

  onPrivateButtonClick(): void{
    this.httpClient.get<Result>(this.serverUri + 'private', {headers: this.headers}).subscribe(
      result => this.privateResponse = result,
      err => this.privateResponse = this.isUnauthorized(err)

    );
  }

  onPermissionsButtonClick(): void{
    this.httpClient.get<Result>(this.serverUri + 'permission', {headers: this.headers}).subscribe(
      result => this.permissionResponse = result,
      err => this.permissionResponse = this.isUnauthorized(err)
    );
  }

  isUnauthorized(err: HttpErrorResponse): Result{
    if (err.status === 401 || err.status === 403){
      return {msg: 'No tienes permisos para ver este contenido.'};
    }
    else{
      return {msg: err.error || 'Ocurrió un error en el servidor'};
    }
  }
}
