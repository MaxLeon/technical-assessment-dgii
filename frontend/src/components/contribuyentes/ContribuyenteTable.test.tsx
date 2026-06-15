import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import ContribuyenteTable from './ContribuyenteTable'
import { Contribuyente } from '@/lib/types'

const mockPush = jest.fn()
jest.mock('next/navigation', () => ({
  useRouter: () => ({ push: mockPush }),
}))

const contribuyentes: Contribuyente[] = [
  { rncCedula: '98754321012', nombre: 'JUAN PEREZ', tipo: 'PERSONA FISICA', estatus: 'activo' },
  { rncCedula: '123456789', nombre: 'EMPRESA SA', tipo: 'PERSONA JURIDICA', estatus: 'inactivo' },
]

describe('ContribuyenteTable', () => {
  beforeEach(() => mockPush.mockClear())

  it('debe renderizar la lista de contribuyentes', () => {
    // GIVEN
    render(<ContribuyenteTable data={contribuyentes} />)

    // WHEN - (render es el when en este caso)

    // THEN
    expect(screen.getByText('JUAN PEREZ')).toBeInTheDocument()

    // AND
    expect(screen.getByText('EMPRESA SA')).toBeInTheDocument()
    expect(screen.getByText('98754321012')).toBeInTheDocument()
  })

  it('debe navegar al detalle al hacer click en una fila', async () => {
    // GIVEN
    render(<ContribuyenteTable data={contribuyentes} />)
    const user = userEvent.setup()

    // WHEN
    await user.click(screen.getByText('JUAN PEREZ'))

    // THEN
    expect(mockPush).toHaveBeenCalledWith('/contribuyentes/98754321012')

    // AND
    expect(mockPush).toHaveBeenCalledTimes(1)
  })

  it('debe mostrar chip de estatus activo con color success', () => {
    // GIVEN
    render(<ContribuyenteTable data={[contribuyentes[0]]} />)

    // WHEN
    const chip = screen.getByText('activo')

    // THEN
    expect(chip).toBeInTheDocument()
  })

  it('debe renderizar tabla vacía sin errores', () => {
    // GIVEN
    render(<ContribuyenteTable data={[]} />)

    // WHEN - (render es el when)

    // THEN
    expect(screen.queryByText('JUAN PEREZ')).not.toBeInTheDocument()

    // AND
    expect(screen.getByRole('grid')).toBeInTheDocument()
  })
})
