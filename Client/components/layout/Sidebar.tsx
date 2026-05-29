"use client";
import Link from "next/link";
import { usePathname } from "next/navigation";
import {
  LayoutDashboard, FileText, PlusCircle,
  BarChart2, Settings, ShieldCheck, LogOut
} from "lucide-react";

const nav = [
  { href: "/dashboard",       icon: LayoutDashboard, label: "Tổng quan" },
  { href: "/applications",    icon: FileText,         label: "Hồ sơ vay" },
  { href: "/applications/new",icon: PlusCircle,       label: "Tạo hồ sơ mới" },
  { href: "/monitoring",      icon: BarChart2,        label: "Monitoring" },
];

export default function Sidebar() {
  const path = usePathname();
  return (
    <aside style={{
      width: 240, minHeight: "100vh", background: "var(--bg-card)",
      borderRight: "1px solid var(--border)", display: "flex",
      flexDirection: "column", padding: "0", flexShrink: 0,
    }}>
      {/* Logo */}
      <div style={{ padding: "24px 20px 20px", borderBottom: "1px solid var(--border)" }}>
        <div style={{ display: "flex", alignItems: "center", gap: 10 }}>
          <div style={{
            width: 36, height: 36, borderRadius: 8,
            background: "linear-gradient(135deg, #3b82f6, #8b5cf6)",
            display: "flex", alignItems: "center", justifyContent: "center",
          }}>
            <ShieldCheck size={20} color="#fff" />
          </div>
          <div>
            <div style={{ fontWeight: 700, fontSize: 16, color: "var(--text)", letterSpacing: "0.04em" }}>CREDO</div>
            <div style={{ fontSize: 11, color: "var(--text-muted)", marginTop: -2 }}>Credit Scoring AI</div>
          </div>
        </div>
      </div>

      {/* Nav */}
      <nav style={{ flex: 1, padding: "12px 10px" }}>
        <div style={{ fontSize: 11, color: "var(--text-muted)", padding: "8px 10px 4px", letterSpacing: "0.08em", textTransform: "uppercase" }}>
          Menu
        </div>
        {nav.map(({ href, icon: Icon, label }) => {
          const active = path === href || (href !== "/dashboard" && path.startsWith(href));
          return (
            <Link key={href} href={href} style={{ textDecoration: "none" }}>
              <div style={{
                display: "flex", alignItems: "center", gap: 10,
                padding: "9px 10px", borderRadius: 8, marginBottom: 2,
                background: active ? "var(--blue-dim)" : "transparent",
                color: active ? "var(--blue-light)" : "var(--text-dim)",
                fontWeight: active ? 600 : 400,
                transition: "all 0.15s",
                cursor: "pointer",
              }}
                onMouseEnter={e => { if (!active) (e.currentTarget as HTMLElement).style.background = "var(--bg-hover)"; }}
                onMouseLeave={e => { if (!active) (e.currentTarget as HTMLElement).style.background = "transparent"; }}
              >
                <Icon size={16} />
                <span style={{ fontSize: 13 }}>{label}</span>
                {active && <div style={{ marginLeft: "auto", width: 6, height: 6, borderRadius: "50%", background: "var(--blue)" }} />}
              </div>
            </Link>
          );
        })}
      </nav>

      {/* Bottom */}
      <div style={{ padding: "12px 10px", borderTop: "1px solid var(--border)" }}>
        <Link href="/settings" style={{ textDecoration: "none" }}>
          <div style={{ display: "flex", alignItems: "center", gap: 10, padding: "9px 10px", borderRadius: 8, color: "var(--text-muted)", cursor: "pointer" }}>
            <Settings size={16} /><span style={{ fontSize: 13 }}>Cài đặt</span>
          </div>
        </Link>
        <div style={{ display: "flex", alignItems: "center", gap: 10, padding: "9px 10px", borderRadius: 8, color: "var(--text-muted)", cursor: "pointer" }}>
          <LogOut size={16} /><span style={{ fontSize: 13 }}>Đăng xuất</span>
        </div>
        <div style={{ marginTop: 12, padding: "10px", background: "var(--bg)", borderRadius: 8, border: "1px solid var(--border)" }}>
          <div style={{ display: "flex", alignItems: "center", gap: 8 }}>
            <div style={{ width: 32, height: 32, borderRadius: "50%", background: "linear-gradient(135deg,#3b82f6,#8b5cf6)", display: "flex", alignItems: "center", justifyContent: "center", fontSize: 13, fontWeight: 700, color: "#fff" }}>A</div>
            <div>
              <div style={{ fontSize: 12, fontWeight: 600, color: "var(--text)" }}>Analyst</div>
              <div style={{ fontSize: 11, color: "var(--text-muted)" }}>Credit Officer</div>
            </div>
          </div>
        </div>
      </div>
    </aside>
  );
}