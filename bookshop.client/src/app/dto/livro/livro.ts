export class LivroRequestDto {
    cod: number = 0;
    editora: string = '';
    edicao: string = '';
    anoPublicacao: string = '';
    sinopse: string = '';
    titulo: string = '';
    autores: Array<number> = [];
    assuntos: Array<number> = [];

    constructor(data?: Partial<LivroRequestDto>) {
        this.cod = data?.cod ?? this.cod;
        this.editora = data?.editora ?? this.editora;
        this.edicao = data?.edicao ?? this.edicao;
        this.anoPublicacao = data?.anoPublicacao ?? this.anoPublicacao;
        this.sinopse = data?.sinopse ?? this.sinopse;
        this.titulo = data?.titulo ?? this.titulo;
        this.autores = data?.autores ?? this.autores;
        this.assuntos = data?.assuntos ?? this.assuntos;
    }
}
