import { ResponseCode } from "../enum/response-code.enum";

export interface HeaderResponse{
  responseCode: ResponseCode
  message: string,
  success: boolean
}
