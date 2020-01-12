import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeroesComponent } from './Modules/HeroesModule/HeroesComponent/Heroes/Heroes.component';
import { PageNotFoundComponent } from './Modules/PageNotFoundModule/PageNotFound/PageNotFound.component';
import { HomeComponent } from './Modules/HomeModule/Home/Home.component';

const routes: Routes = [
   { path: '', component: HomeComponent},
   { path: 'heroes', component: HeroesComponent },
   { path: '**', component: PageNotFoundComponent }
   //{ path: '', component: AppComponent, pathMatch: 'full'}
];

@NgModule({
   imports: [
      RouterModule.forRoot
      (
         routes,
         { enableTracing: true }
      )
   ],
   exports: [
      RouterModule
   ],
   declarations: [
   ]
})
export class AppRoutingModule { }
