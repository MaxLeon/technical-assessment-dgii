import { render, screen } from '@testing-library/react'
import ComprobanteTable from './ComprobanteTable'
import { ComprobanteFiscal } from '@/lib/types'

const comprobantes: ComprobanteFiscal[] = [
  { id: 1, rncCedula: '98754321012', ncf: 'E310000000001', monto: 200.00, itbis18: 36.00 },
  { id: 2, rncCedula: '98754321012', ncf: 'E310000000002', monto: 500.00, itbis18: 90.00 },
]

describe('ComprobanteTable', () => {
  it('debe renderizar todos los comprobantes del contribuyente', () => {
    // GIVEN
    render(<ComprobanteTable data={comprobantes} />)

    // WHEN - (render es el when)

    // THEN
    expect(screen.getByText('E310000000001')).toBeInTheDocument()

    // AND
    expect(screen.getByText('E310000000002')).toBeInTheDocument()
  })

  it('debe mostrar tabla vacía sin comprobantes', () => {
    // GIVEN
    render(<ComprobanteTable data={[]} />)

    // WHEN - (render es el when)

    // THEN
    expect(screen.queryByText('E310000000001')).not.toBeInTheDocument()

    // AND
    expect(screen.getByRole('grid')).toBeInTheDocument()
  })
})
