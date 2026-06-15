import { ApiResponse, ComprobanteFiscal, Contribuyente } from './types'

const BASE_URL = typeof window === 'undefined'
  ? (process.env.API_URL ?? 'http://backend:8080/api')
  : (process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5002/api')

async function fetchApi<T>(path: string, options?: RequestInit): Promise<T> {
  const res = await fetch(`${BASE_URL}${path}`, {
    headers: { 'Content-Type': 'application/json' },
    // Evitar cache para asegurar que siempre se obtienen los datos más recientes
    cache: 'no-store',
    ...options,
  })

  const body: ApiResponse<T> = await res.json()

  if (!body.success || body.data === null) {
    throw new Error(body.error?.message ?? 'Error desconocido')
  }

  return body.data
}

export const contribuyentesApi = {
  GetContribuyentes: () => fetchApi<Contribuyente[]>('/contribuyentes'),
  GetByRncCedula: (rnc: string) => fetchApi<Contribuyente>(`/contribuyentes/${rnc}`),
}

export const comprobantesApi = {
  GetByRncCedula: (rnc: string) => fetchApi<ComprobanteFiscal[]>(`/comprobantes/contribuyente/${rnc}`),
  GetTotalItbis: (rnc: string) => fetchApi<{ totalItbis: number }>(`/comprobantes/contribuyente/${rnc}/total-itbis`),
}
