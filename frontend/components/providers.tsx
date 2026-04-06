// Run in the browser:
"use client"

import { useState } from "react"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { ReactQueryDevtools } from "@tanstack/react-query-devtools"

export function Providers({ children }: { children: React.ReactNode }) {
    // Create a QueryClient per user.
    const [queryClient] = useState(() => new QueryClient())

    return (
        // Makes the query accesible for all the other components.
        <QueryClientProvider client={queryClient}>
            {children}
            <ReactQueryDevtools />
        </QueryClientProvider>
    )
}