import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeroesComponent } from './Modules/HeroesModule/HeroesComponent/Heroes/Heroes.component';

const routes: Routes = [
   { path: 'heroes', component: HeroesComponent },
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
