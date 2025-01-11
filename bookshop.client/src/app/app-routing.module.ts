import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LivrosComponent } from './forms/livros/livros.component';
import { LivrosAddComponent } from './components/livros-add/livros-add.component';
import { LivrosEditComponent } from './components/livros-edit/livros-edit.component';
import { AssuntosComponent } from './forms/assuntos/assuntos.component';
import { AssuntosAddComponent } from './components/assuntos-add/assuntos-add.component';
import { AssuntosEditComponent } from './components/assuntos-edit/assuntos-edit.component';
import { AutoresComponent } from './forms/autores/autores.component';
import { AutoresAddComponent } from './components/autores-add/autores-add.component';
import { AutoresEditComponent } from './components/autores-edit/autores-edit.component';
import { RelatorioComponent } from './components/relatorio/relatorio.component';
import { CanaisComponent } from './forms/canais/canais.component';
import { CanaisAddComponent } from './components/canais-add/canais-add.component';
import { CanaisEditComponent } from './components/canais-edit/canais-edit.component';
import { CanalPrecosComponent } from './forms/canal-precos/canal-precos.component';
import { CanalPrecosAddComponent } from './components/canal-precos-add/canal-precos-add.component';
import { CanalPrecosEditComponent } from './components/canal-precos-edit/canal-precos-edit.component';


export const routes: Routes = [
  { path: 'livros', component: LivrosComponent },
  { path: 'livrosAdd', component: LivrosAddComponent },
  { path: 'livrosEdit/:cod', component: LivrosEditComponent },
  { path: 'assuntos', component: AssuntosComponent },
  { path: 'assuntosAdd', component: AssuntosAddComponent },
  { path: 'assuntosEdit/:cod', component: AssuntosEditComponent },
  { path: 'autores', component: AutoresComponent },
  { path: 'autoresAdd', component: AutoresAddComponent },
  { path: 'autoresEdit/:cod', component: AutoresEditComponent },
  { path: 'relatorio', component: RelatorioComponent },
  { path: 'canais', component: CanaisComponent },
  { path: 'canaisAdd', component: CanaisAddComponent },
  { path: 'canaisEdit/:cod', component: CanaisEditComponent },
  { path: 'canalPrecos', component: CanalPrecosComponent },
  { path: 'canalPrecosAdd', component: CanalPrecosAddComponent },
  { path: 'canalPrecosEdit/:cod', component: CanalPrecosEditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
