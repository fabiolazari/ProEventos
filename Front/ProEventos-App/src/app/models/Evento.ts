import { Lote } from "./Lote"
import { Palestrante } from "./Palestrante";
//import { PalestranteEvento } from "./PalestranteEvento"
import { RedeSocial } from "./RedeSocial"

export interface Evento {
  id: number;
  local:  string
  dataEvento?: Date;
  tema: string;
  qtdPessoas: number;
  imageURL: string;
  telefone: string;
  email: string;
  lotes: Lote[];
  redesSociais: RedeSocial[];
  palestrantesEventos: Palestrante[]//PalestranteEvento[]
}
