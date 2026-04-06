import { auth } from "@/auth"

export async function fetchApi<T>(url: string, options?: RequestInit): Promise<T> {
    // Collects the session and access Token:
    const session = await auth()
    const accessToken = (session as any)?.accessToken

    const response = await fetch(`http://localhost:5285${url}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...(accessToken ? { Authorization: `Bearer ${accessToken}` } : {}),
            ...options?.headers,
        },
    })

    if (!response.ok) {
        throw new Error(`API error: ${response.status}`)
    }

    return response.json()
}