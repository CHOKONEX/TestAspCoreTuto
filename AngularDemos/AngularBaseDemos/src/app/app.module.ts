import { HomeComponent } from './Modules/HomeModule/Home/Home.component';
import { PageNotFoundComponent } from './Modules/PageNotFoundModule/PageNotFound/PageNotFound.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeroesComponent } from './Modules/HeroesModule/HeroesComponent/Heroes/Heroes.component';

@NgModule({
  declarations: [
    AppComponent, HomeComponent,
    HeroesComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
