"use client";
import { ShieldCheck, Eye, EyeOff } from "lucide-react";
import { useState } from "react";

export default function LoginPage() {
  const [show, setShow] = useState(false);
  return (
    <div style={{ minHeight:"100vh", background:"var(--bg)", display:"flex", alignItems:"center", justifyContent:"center" }}>
      <div style={{ width:380 }} className="animate-in">

        {/* Logo */}
        <div style={{ textAlign:"center", marginBottom:32 }}>
          <div style={{ width:56, height:56, borderRadius:14, background:"linear-gradient(135deg,#3b82f6,#8b5cf6)", display:"flex", alignItems:"center", justifyContent:"center", margin:"0 auto 12px" }}>
            <ShieldCheck size={28} color="#fff"/>
          </div>
          <div style={{ fontSize:22, fontWeight:700, color:"var(--text)" }}>CREDO</div>
          <div style={{ fontSize:13, color:"var(--text-muted)", marginTop:4 }}>Hệ thống đánh giá tín dụng cá nhân</div>
        </div>

        {/* Card */}
        <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:28 }}>
          <div style={{ fontSize:16, fontWeight:600, marginBottom:20 }}>Đăng nhập</div>

          <div style={{ marginBottom:16 }}>
            <label style={{ display:"block", fontSize:12, color:"var(--text-dim)", marginBottom:6 }}>Tài khoản</label>
            <input placeholder="Nhập tên đăng nhập" style={{ width:"100%", padding:"9px 12px", background:"var(--bg)", border:"1px solid var(--border)", borderRadius:8, color:"var(--text)", fontSize:13, outline:"none" }} />
          </div>

          <div style={{ marginBottom:20 }}>
            <label style={{ display:"block", fontSize:12, color:"var(--text-dim)", marginBottom:6 }}>Mật khẩu</label>
            <div style={{ position:"relative" }}>
              <input type={show?"text":"password"} placeholder="Nhập mật khẩu"
                style={{ width:"100%", padding:"9px 36px 9px 12px", background:"var(--bg)", border:"1px solid var(--border)", borderRadius:8, color:"var(--text)", fontSize:13, outline:"none" }} />
              <div onClick={()=>setShow(!show)} style={{ position:"absolute", right:10, top:"50%", transform:"translateY(-50%)", cursor:"pointer", color:"var(--text-muted)" }}>
                {show ? <EyeOff size={15}/> : <Eye size={15}/>}
              </div>
            </div>
          </div>

          <a href="/dashboard" style={{ textDecoration:"none" }}>
            <button style={{ width:"100%", padding:"10px", background:"linear-gradient(135deg,#3b82f6,#8b5cf6)", border:"none", borderRadius:8, color:"#fff", fontSize:14, fontWeight:600, cursor:"pointer" }}>
              Đăng nhập →
            </button>
          </a>

          <div style={{ textAlign:"center", marginTop:16, fontSize:12, color:"var(--text-muted)" }}>
            Hệ thống nội bộ — Chỉ dành cho nhân viên được cấp phép
          </div>
        </div>
      </div>
    </div>
  );
}