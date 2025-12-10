import { HeaderResponse } from "./header-response.interface";

export interface GenericResponse<T>{
  header: HeaderResponse,
  data: T
}
