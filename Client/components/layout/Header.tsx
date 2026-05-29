"use client";
import { Bell, Search } from "lucide-react";

interface HeaderProps { title: string; subtitle?: string; }

export default function Header({ title, subtitle }: HeaderProps) {
  return (
    <header style={{
      height: 60, background: "var(--bg-card)", borderBottom: "1px solid var(--border)",
      display: "flex", alignItems: "center", padding: "0 24px", gap: 16,
      position: "sticky", top: 0, zIndex: 10,
    }}>
      <div style={{ flex: 1 }}>
        <div style={{ fontWeight: 700, fontSize: 15, color: "var(--text)" }}>{title}</div>
        {subtitle && <div style={{ fontSize: 12, color: "var(--text-muted)", marginTop: -2 }}>{subtitle}</div>}
      </div>

      {/* Search */}
      <div style={{
        display: "flex", alignItems: "center", gap: 8,
        background: "var(--bg)", border: "1px solid var(--border)",
        borderRadius: 8, padding: "6px 12px", width: 240,
      }}>
        <Search size={14} color="var(--text-muted)" />
        <input placeholder="Tìm kiếm hồ sơ, CMND..." style={{
          background: "transparent", border: "none", outline: "none",
          color: "var(--text)", fontSize: 13, width: "100%",
        }} />
      </div>

      {/* Bell */}
      <div style={{ position: "relative", cursor: "pointer" }}>
        <Bell size={18} color="var(--text-muted)" />
        <div style={{
          position: "absolute", top: -4, right: -4, width: 16, height: 16,
          background: "var(--red)", borderRadius: "50%", fontSize: 10,
          display: "flex", alignItems: "center", justifyContent: "center",
          color: "#fff", fontWeight: 700,
        }}>3</div>
      </div>
    </header>
  );
}