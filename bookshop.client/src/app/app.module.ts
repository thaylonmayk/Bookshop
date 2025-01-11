import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbAlertModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';

import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LivrosComponent } from './forms/livros/livros.component';
import { LivrosAddComponent } from './components/livros-add/livros-add.component';
import { LivrosEditComponent } from './components/livros-edit/livros-edit.component';
import { AutoresComponent } from './forms/autores/autores.component';
import { AutoresAddComponent } from './components/autores-add/autores-add.component';
import { AutoresEditComponent } from './components/autores-edit/autores-edit.component';
import { AssuntosComponent } from './forms/assuntos/assuntos.component';
import { AssuntosAddComponent } from './components/assuntos-add/assuntos-add.component';
import { AssuntosEditComponent } from './components/assuntos-edit/assuntos-edit.component';
import { RelatorioComponent } from './components/relatorio/relatorio.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ErrorInterceptor } from '../services/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { provideRouter } from '@angular/router';
import { CanaisComponent } from './forms/canais/canais.component';
import { CanaisAddComponent } from './components/canais-add/canais-add.component';
import { CanaisEditComponent } from './components/canais-edit/canais-edit.component';
import { CanalPrecosComponent } from './forms/canal-precos/canal-precos.component';
import { CanalPrecosAddComponent } from './components/canal-precos-add/canal-precos-add.component';
import { CanalPrecosEditComponent } from './components/canal-precos-edit/canal-precos-edit.component';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    AppComponent,
    NavbarComponent,
    LivrosComponent,
    LivrosAddComponent,
    LivrosEditComponent,
    AutoresComponent,
    AutoresAddComponent,
    AutoresEditComponent,
    AssuntosComponent,
    AssuntosAddComponent,
    AssuntosEditComponent,
    RelatorioComponent,
    CanaisComponent,
    CanaisAddComponent,
    CanaisEditComponent,
    CanalPrecosComponent,
    CanalPrecosAddComponent,
    CanalPrecosEditComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgbAlertModule,
    ToastrModule.forRoot({
      positionClass: 'toast-center-center', // Custom position class
      timeOut: 3000, // Duration of the toast
      closeButton: true, // Show close button
      progressBar: true // Show progress bar)
    })],
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
