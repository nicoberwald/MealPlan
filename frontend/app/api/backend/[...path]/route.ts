import { auth } from "@/auth"
import { NextRequest, NextResponse } from "next/server"

async function handler(req: NextRequest, { params }: { params: Promise<{ path: string[] }> }) {
    const session = await auth()
    const accessToken = (session as any)?.accessToken

    const { path } = await params
    const url = new URL(req.url)
    const backendUrl = `http://localhost:5285/${path.join("/")}${url.search}`

    const response = await fetch(backendUrl, {
        method: req.method,
        headers: {
            "Content-Type": "application/json",
            ...(accessToken ? { Authorization: `Bearer ${accessToken}` } : {}),
        },
        body: req.method !== "GET" && req.method !== "HEAD" ? await req.text() : undefined,
    })

    if (response.status === 204) {
        return new NextResponse(null, { status: 204 })
    }

    const data = await response.json()
    return NextResponse.json(data, { status: response.status })
}

export { handler as GET, handler as POST, handler as PUT, handler as DELETE }
