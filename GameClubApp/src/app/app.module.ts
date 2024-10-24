import { CommonModule, CurrencyPipe, DatePipe } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule, APP_INITIALIZER, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from "@angular/material/form-field";
import { MAT_RADIO_DEFAULT_OPTIONS } from "@angular/material/radio";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule, NoopAnimationsModule } from "@angular/platform-browser/animations";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { AuthGuard } from "./guards/auth.guard";
import { MaterialModule } from "./material.module";
import { ClubsModule } from "./modules/clubs/clubs.module";
import { SharedModule } from "./shared.module";
import { appRoutes } from "./app.routes";
import { HeaderComponent } from "./components/header/header.component";


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    BrowserModule, //.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, { onSameUrlNavigation: 'reload' }),
    RouterModule.forChild([]),
    BrowserAnimationsModule,
    NoopAnimationsModule,
    MaterialModule,
    SharedModule,

    //app modules
    ClubsModule
  ],
  exports: [RouterModule],
  providers: [
    AuthGuard,
    CurrencyPipe,
    DatePipe,
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'fill' }
    },
    {
      provide: MAT_RADIO_DEFAULT_OPTIONS,
      useValue: { color: 'accent' },
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initWithDependencyFactory,
      deps: [],
      multi: true
    },
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }


export function initWithDependencyFactory(
) {
  return () => {
    console.log('initWithDependencyFactory - started');
    return true;
  }
}
