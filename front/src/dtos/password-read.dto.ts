import { ApplicationReadDto } from "./application-read.dto";

export interface PasswordReadDto {
    password_id: number;
    account_name: string;
    password: string;
    application: ApplicationReadDto;
  }
  