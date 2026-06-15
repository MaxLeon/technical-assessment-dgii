import { render, screen } from '@testing-library/react'
import ItbisSummaryCard from './ItbisSummaryCard'

describe('ItbisSummaryCard', () => {
  it('debe mostrar el total ITBIS formateado con dos decimales', () => {
    // GIVEN
    const total = 126

    // WHEN
    render(<ItbisSummaryCard total={total} />)

    // THEN
    expect(screen.getByText('RD$ 126.00')).toBeInTheDocument()

    // AND
    expect(screen.getByText('Total ITBIS')).toBeInTheDocument()
  })

  it('debe mostrar cero cuando no hay comprobantes', () => {
    // GIVEN
    const total = 0

    // WHEN
    render(<ItbisSummaryCard total={total} />)

    // THEN
    expect(screen.getByText('RD$ 0.00')).toBeInTheDocument()
  })

  it('debe formatear correctamente montos con decimales', () => {
    // GIVEN
    const total = 36.5

    // WHEN
    render(<ItbisSummaryCard total={total} />)

    // THEN
    expect(screen.getByText('RD$ 36.50')).toBeInTheDocument()
  })
})
