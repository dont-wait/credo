import Header from "@/components/layout/Header";
import { FileText, CheckCircle, XCircle, Clock, TrendingUp, AlertTriangle } from "lucide-react";
import Link from "next/link";
const stats = [
  { label: "Tổng hồ sơ hôm nay", value: "48", icon: FileText,     color: "var(--blue)",   bg: "var(--blue-dim)" },
  { label: "Đã phê duyệt",        value: "31", icon: CheckCircle,  color: "var(--green)",  bg: "var(--green-dim)" },
  { label: "Từ chối",              value: "9",  icon: XCircle,      color: "var(--red)",    bg: "var(--red-dim)" },
  { label: "Chờ xem xét",         value: "8",  icon: Clock,        color: "var(--orange)", bg: "var(--orange-dim)" },
];

const recentApps = [
  { id: "APP-2026-0048", name: "Nguyễn Văn An",   score: 712, group: "B", decision: "APPROVED",      time: "14:32" },
  { id: "APP-2026-0047", name: "Trần Thị Bích",   score: 634, group: "C", decision: "CONDITIONAL",   time: "14:18" },
  { id: "APP-2026-0046", name: "Lê Minh Khoa",    score: 428, group: "E", decision: "REJECTED",      time: "13:55" },
  { id: "APP-2026-0045", name: "Phạm Thị Lan",    score: 521, group: "D", decision: "MANUAL_REVIEW", time: "13:40" },
  { id: "APP-2026-0044", name: "Hoàng Văn Dũng",  score: 775, group: "A", decision: "APPROVED",      time: "13:22" },
  { id: "APP-2026-0043", name: "Vũ Thị Mai",      score: 688, group: "B", decision: "APPROVED",      time: "13:05" },
];

const groupColors: Record<string, { color: string; bg: string }> = {
  A: { color: "var(--green)",  bg: "var(--green-dim)" },
  B: { color: "var(--blue)",   bg: "var(--blue-dim)"  },
  C: { color: "var(--orange)", bg: "var(--orange-dim)"},
  D: { color: "#f59e0b",       bg: "#451a03"          },
  E: { color: "var(--red)",    bg: "var(--red-dim)"   },
};

const decisionLabel: Record<string, { label: string; color: string }> = {
  APPROVED:      { label: "Phê duyệt",       color: "var(--green)"  },
  REJECTED:      { label: "Từ chối",          color: "var(--red)"    },
  CONDITIONAL:   { label: "Có điều kiện",    color: "var(--orange)" },
  MANUAL_REVIEW: { label: "Xem xét thủ công",color: "#a78bfa"       },
};

const modelMetrics = [
  { label: "Gini",    value: "0.58", status: "good"    },
  { label: "KS",      value: "0.41", status: "good"    },
  { label: "AUC-ROC", value: "0.79", status: "good"    },
  { label: "PSI",     value: "0.08", status: "good"    },
];

export default function DashboardPage() {
  return (
    <div style={{ minHeight: "100vh" }}>
      <Header title="Tổng quan" subtitle="Thứ Sáu, 29 tháng 5 năm 2026" />

      <div style={{ padding: 24 }} className="animate-in">

        {/* Stats */}
        <div style={{ display: "grid", gridTemplateColumns: "repeat(4,1fr)", gap: 16, marginBottom: 24 }}>
          {stats.map(({ label, value, icon: Icon, color, bg }) => (
            <div key={label} style={{
              background: "var(--bg-card)", border: "1px solid var(--border)",
              borderRadius: "var(--radius)", padding: "20px",
            }}>
              <div style={{ display: "flex", justifyContent: "space-between", alignItems: "flex-start" }}>
                <div>
                  <div style={{ fontSize: 12, color: "var(--text-muted)", marginBottom: 6 }}>{label}</div>
                  <div style={{ fontSize: 32, fontWeight: 700, color }}>{value}</div>
                </div>
                <div style={{ width: 40, height: 40, borderRadius: 10, background: bg, display: "flex", alignItems: "center", justifyContent: "center" }}>
                  <Icon size={20} color={color} />
                </div>
              </div>
            </div>
          ))}
        </div>

        <div style={{ display: "grid", gridTemplateColumns: "1fr 320px", gap: 16 }}>

          {/* Recent Applications */}
          <div style={{ background: "var(--bg-card)", border: "1px solid var(--border)", borderRadius: "var(--radius)" }}>
            <div style={{ padding: "16px 20px", borderBottom: "1px solid var(--border)", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
              <div style={{ fontWeight: 600, fontSize: 14 }}>Hồ sơ gần đây</div>
              <a href="/applications" style={{ fontSize: 12, color: "var(--blue)", textDecoration: "none" }}>Xem tất cả →</a>
            </div>
            <table style={{ width: "100%", borderCollapse: "collapse" }}>
              <thead>
                <tr style={{ borderBottom: "1px solid var(--border)" }}>
                  {["Mã hồ sơ","Khách hàng","Credit Score","Nhóm","Quyết định","Thời gian"].map(h => (
                    <th key={h} style={{ padding: "10px 20px", textAlign: "left", fontSize: 12, color: "var(--text-muted)", fontWeight: 500 }}>{h}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {recentApps.map((app, i) => {
                  const g = groupColors[app.group];
                  const d = decisionLabel[app.decision];
                  return (
                    <tr key={app.id} style={{ borderBottom: i < recentApps.length - 1 ? "1px solid var(--border)" : "none" }}>
                      <td style={{ padding: "12px 20px", fontSize: 13, color: "var(--blue-light)", fontFamily: "monospace" }}>
                        <Link href={`/scoring/${app.id}`} style={{ textDecoration:"none", color:"var(--blue-light)" }}>
                          {app.id}
                        </Link>
                      </td>                      
                      <td style={{ padding: "12px 20px", fontSize: 13, fontWeight: 500 }}>{app.name}</td>
                      <td style={{ padding: "12px 20px", fontSize: 13, fontWeight: 700 }}>{app.score}</td>
                      <td style={{ padding: "12px 20px" }}>
                        <span style={{ background: g.bg, color: g.color, padding: "2px 10px", borderRadius: 20, fontSize: 12, fontWeight: 700 }}>
                          Nhóm {app.group}
                        </span>
                      </td>
                      <td style={{ padding: "12px 20px" }}>
                        <span style={{ color: d.color, fontSize: 12, fontWeight: 500 }}>{d.label}</span>
                      </td>
                      <td style={{ padding: "12px 20px", fontSize: 12, color: "var(--text-muted)" }}>{app.time}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>

          {/* Right column */}
          <div style={{ display: "flex", flexDirection: "column", gap: 16 }}>

            {/* Model Health */}
            <div style={{ background: "var(--bg-card)", border: "1px solid var(--border)", borderRadius: "var(--radius)", padding: 20 }}>
              <div style={{ display: "flex", alignItems: "center", justifyContent: "space-between", marginBottom: 16 }}>
                <div style={{ fontWeight: 600, fontSize: 14 }}>Model Health</div>
                <span style={{ background: "var(--green-dim)", color: "var(--green)", fontSize: 11, padding: "2px 8px", borderRadius: 20, fontWeight: 600 }}>● STABLE</span>
              </div>
              {modelMetrics.map(m => (
                <div key={m.label} style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: 12 }}>
                  <div style={{ fontSize: 13, color: "var(--text-dim)" }}>{m.label}</div>
                  <div style={{ display: "flex", alignItems: "center", gap: 6 }}>
                    <div style={{ fontSize: 14, fontWeight: 700, color: "var(--green)" }}>{m.value}</div>
                    <TrendingUp size={12} color="var(--green)" />
                  </div>
                </div>
              ))}
              <a href="/monitoring" style={{ display: "block", textAlign: "center", marginTop: 8, fontSize: 12, color: "var(--blue)", textDecoration: "none" }}>
                Xem chi tiết monitoring →
              </a>
            </div>

            {/* Manual Review Queue */}
            <div style={{ background: "var(--bg-card)", border: "1px solid var(--border)", borderRadius: "var(--radius)", padding: 20 }}>
              <div style={{ display: "flex", alignItems: "center", gap: 8, marginBottom: 16 }}>
                <AlertTriangle size={16} color="var(--orange)" />
                <div style={{ fontWeight: 600, fontSize: 14 }}>Hàng đợi xem xét</div>
                <span style={{ marginLeft: "auto", background: "var(--orange-dim)", color: "var(--orange)", fontSize: 11, padding: "2px 8px", borderRadius: 20, fontWeight: 700 }}>8</span>
              </div>
              {[
                { id: "APP-2026-0045", name: "Phạm Thị Lan",   score: 521, sla: "18h còn lại" },
                { id: "APP-2026-0041", name: "Đỗ Văn Hùng",    score: 498, sla: "6h còn lại"  },
                { id: "APP-2026-0039", name: "Ngô Thị Hoa",    score: 535, sla: "2h còn lại"  },
              ].map(item => (
                <div key={item.id} style={{ padding: "10px 0", borderBottom: "1px solid var(--border)", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                  <div>
                    <div style={{ fontSize: 12, fontWeight: 500 }}>{item.name}</div>
                    <div style={{ fontSize: 11, color: "var(--text-muted)", fontFamily: "monospace" }}>{item.id}</div>
                  </div>
                  <div style={{ textAlign: "right" }}>
                    <div style={{ fontSize: 13, fontWeight: 700, color: "var(--orange)" }}>{item.score}</div>
                    <div style={{ fontSize: 11, color: "var(--text-muted)" }}>{item.sla}</div>
                  </div>
                </div>
              ))}
              <a href="/applications?status=manual" style={{ display: "block", textAlign: "center", marginTop: 12, fontSize: 12, color: "var(--orange)", textDecoration: "none" }}>
                Xem tất cả →
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}