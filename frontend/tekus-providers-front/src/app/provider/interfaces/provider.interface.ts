export interface Provider {
  id: string;
  nit: string;
  name: string;
  email: string;
  customFields?: { [key: string]: any } | null;
  createdAt: string;
  updatedAt?: string | null;
}
