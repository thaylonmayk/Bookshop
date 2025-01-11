import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { CanalPrecosService } from '../../../services/canal-precos.service';
import { LivrosService } from '../../../services/livros.service';
import { CanaisService } from '../../../services/canais.service';

@Component({
  selector: 'app-canal-precos-add',
  standalone: false,

  templateUrl: './canal-precos-add.component.html',
  styleUrl: './canal-precos-add.component.css',
})
export class CanalPrecosAddComponent {
  livros: any[] = [];
  canais: any[] = [];
  livroSelecionado = new FormControl();
  canalSelecionado = new FormControl();
  preco = new FormControl();

  constructor(
    private readonly livroService: LivrosService,
    private readonly canaisService: CanaisService,
    private readonly canalPrecosService: CanalPrecosService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getLivros();
    this.getCanais();
  }

  getLivros() {
    this.livroService.getLivros().subscribe((livros: any) => {
      this.livros = livros;
    });
  }

  getCanais() {
    this.canaisService.getCanais().subscribe((canais: any) => {
      this.canais = canais;
    });
  }

  addCanalPreco() {
    const canalPreco = {
      cod: 0, 
      valor: this.preco.value,
      codLivro: this.livroSelecionado.value,
      codCanal: this.canalSelecionado.value,
    };

    const { cod, ...canalPrecoWithoutCod } = canalPreco;

    this.canalPrecosService.addCanalPrecos(canalPrecoWithoutCod).subscribe(() => {
      this.router.navigate(['/canalPrecos']);
    });
  }
}
