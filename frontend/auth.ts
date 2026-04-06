import NextAuth from "next-auth"                                                                                                                 
  import Credentials from "next-auth/providers/credentials"                                                                                        
                                                                                                                                                   
  export const { handlers, signIn, signOut, auth } = NextAuth({                                                                                    
    providers: [
      Credentials({                                                                                                                                
        credentials: {                                                                                                                           
          email: { label: "Email", type: "email" },
          password: { label: "Password", type: "password" },
        },
        authorize: async (credentials) => {
          const response = await fetch("http://localhost:5285/login", {                                                                            
            method: "POST",
            headers: { "Content-Type": "application/json" },                                                                                       
            body: JSON.stringify({                                                                                                               
              email: credentials.email,                                                                                                            
              password: credentials.password,
            }),                                                                                                                                    
          })                                                                                                                                     

          if (!response.ok) return null

          const user = await response.json()
          return user
        },
      }),
    ],                                                                                                                                             
    pages: {
      signIn: "/login",                                                                                                                            
    },                                                                                                                                           
  })