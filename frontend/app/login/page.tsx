import { signIn } from "@/auth"
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

export default function LoginPage() {
    return (
        <div className="flex min-h-screen flex-col items-center justify-center bg-slate-900">
            <div className="mb-8 text-center">
                <h1 className="text-3xl font-bold text-white">Smart Meal Planner</h1>
                <p className="mt-2 text-sm text-slate-400">Built by Nicolas Berwald Sørensen</p>
            </div>
            <Card className="w-full max-w-sm border-slate-800 bg-slate-950 shadow-2xl">
                <CardHeader>
                    <CardTitle className="text-center text-white">Created for fun</CardTitle>
                </CardHeader>
                <CardContent>
                    <form
                        action={async (formData) => {
                            "use server"
                            await signIn("credentials", formData)
                        }}
                        className="flex flex-col gap-4"
                    >
                        <Input
                            name="email"
                            type="email"
                            placeholder="Email"
                            required
                            className="border-slate-700 bg-slate-800 text-white placeholder:text-slate-500"
                        />
                        <Input
                            name="password"
                            type="password"
                            placeholder="Password"
                            required
                            className="border-slate-700 bg-slate-800 text-white placeholder:text-slate-500"
                        />
                        <Button type="submit" className="  w-full">
                            Login
                        </Button>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}