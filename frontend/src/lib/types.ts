export interface Contribuyente {
  rncCedula: string
  nombre: string
  tipo: string
  estatus: string
}

export interface ComprobanteFiscal {
  id: number
  rncCedula: string
  ncf: string
  monto: number
  itbis18: number
}

export interface ApiResponse<T> {
  success: boolean
  data: T | null
  error: { message: string } | null
}
