import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AutoresService } from '../../../services/autores.service';
import { AssuntosService } from '../../../services/assuntos.service';
import { LivrosService } from '../../../services/livros.service';
import { LivroRequestDto } from '../../dto/livro/livro';

@Component({
  selector: 'app-livros-add',
  standalone: false,

  templateUrl: './livros-add.component.html',
  styleUrl: './livros-add.component.css',
})
export class LivrosAddComponent {
  autores: any[] = [];
  assuntos: any[] = [];

  autoresSelecionados = new FormControl();
  assuntosSelecionados = new FormControl();
  titulo = new FormControl();
  editora = new FormControl();
  edicao = new FormControl();
  anoPublicacao = new FormControl();
  sinopse = new FormControl();
  valor = new FormControl();

  constructor(
    private readonly autoresService: AutoresService,
    private readonly assuntosService: AssuntosService,
    private readonly livrosService: LivrosService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getAutores();
    this.getAssuntos();
  }

  getAutores() {
    this.autoresService.getAutores().subscribe((autores: any) => {
      this.autores = autores;
    });
  }

  getAssuntos() {
    this.assuntosService.getAssuntos().subscribe((assuntos: any) => {
      this.assuntos = assuntos;
    });
  }

  addLivro() {
    const livro = {
      cod: 0, 
      titulo: this.titulo.value,
      editora: this.editora.value,
      edicao: this.edicao.value,
      anoPublicacao: this.anoPublicacao.value,
      sinopse: this.sinopse.value,
      autores: this.autoresSelecionados.value,
      assuntos: this.assuntosSelecionados.value,
    };

    const { cod, ...livroWithoutCod } = livro;

    this.livrosService.addLivro(livroWithoutCod as LivroRequestDto).subscribe(() => {
      this.router.navigate(['/livros']);
    });
  }
}
