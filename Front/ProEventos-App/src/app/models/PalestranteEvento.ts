import { Evento } from "./Evento";
import { Palestrante } from "./Palestrante";

export interface PalestranteEvento {
  palestranteId: number;
  eventoId: number;
  palestrante: Palestrante;
  evento : Evento
}
