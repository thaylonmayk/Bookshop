import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CanalPrecosService } from '../../../services/canal-precos.service';

@Component({
  selector: 'app-canal-precos',
  standalone: false,

  templateUrl: './canal-precos.component.html',
  styleUrl: './canal-precos.component.css',
})
export class CanalPrecosComponent {
  canalPrecos: any[] = [];

  constructor(
    private readonly canalPrecosService: CanalPrecosService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getCanalPrecos();
  }

  getCanalPrecos() {
    this.canalPrecosService.getCanalPrecos().subscribe((response: any) => {
      this.canalPrecos = response;
      this.canalPrecos.forEach((canalPreco) => {
        canalPreco.assuntos = canalPreco.assuntos
          .map((canalPrecoAssunto: any) => canalPrecoAssunto.assunto.descricao)
          .join(', ');
        canalPreco.autores = canalPreco.autores
          .map((canalPrecoAutor: any) => canalPrecoAutor.autor.nome)
          .join(', ');
      });
    });
  }

  addCanalPreco() {
    this.router.navigate(['/canalPrecosAdd']);
  }

  deleteCanalPreco(cod: number) {
    this.canalPrecosService.deleteCanalPrecos(cod).subscribe(() => {
      this.getCanalPrecos();
    });
  }
}
