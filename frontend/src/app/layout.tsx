import type { Metadata } from 'next'

export const metadata: Metadata = {
  title: 'DGII - Sistema Fiscal',
  description: 'Gestión de contribuyentes y comprobantes fiscales',
}

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="es">
      <body>{children}</body>
    </html>
  )
}
