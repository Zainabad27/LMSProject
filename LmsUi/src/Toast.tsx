import { useState, useCallback, useEffect, type CSSProperties, type ReactNode } from "react";

// ─── Types ────────────────────────────────────────────────────────────────────
type ToastType = "default" | "success" | "error" | "warning" | "info";
type Position =
  | "top-right" | "top-left" | "top-center"
  | "bottom-right" | "bottom-left" | "bottom-center";

interface ToastOptions {
  type?: ToastType;
  duration?: number;
}

interface ToastData extends Required<ToastOptions> {
  id: number;
  message: string;
}

// ─── Toast Store (singleton outside React) ────────────────────────────────────
let listeners: Array<(t: ToastData[]) => void> = [];
let toasts: ToastData[] = [];

function emit(toast: ToastData) {
  toasts = [...toasts, toast];
  listeners.forEach((fn) => fn(toasts));
}

function dismiss(id: number) {
  toasts = toasts.filter((t) => t.id !== id);
  listeners.forEach((fn) => fn(toasts));
}

let uid = 0;
export const toast = {
  show: (message: string, opts: ToastOptions = {}): number => {
    const id = ++uid;
    emit({ id, message, type: "default", duration: 3500, ...opts });
    return id;
  },
  success: (message: string, opts?: ToastOptions) => toast.show(message, { type: "success", ...opts }),
  error:   (message: string, opts?: ToastOptions) => toast.show(message, { type: "error",   ...opts }),
  warning: (message: string, opts?: ToastOptions) => toast.show(message, { type: "warning", ...opts }),
  info:    (message: string, opts?: ToastOptions) => toast.show(message, { type: "info",    ...opts }),
  dismiss,
};

// ─── useToasts hook ───────────────────────────────────────────────────────────
function useToasts(): ToastData[] {
  const [items, setItems] = useState<ToastData[]>(toasts);
  useEffect(() => {
    listeners.push(setItems);
    return () => { listeners = listeners.filter((l) => l !== setItems); };
  }, []);
  return items;
}

// ─── Icons ────────────────────────────────────────────────────────────────────
const icons: Record<ToastType, ReactNode> = {
  success: (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
      <circle cx="8" cy="8" r="7" stroke="currentColor" strokeWidth="1.5"/>
      <path d="M5 8l2 2 4-4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
    </svg>
  ),
  error: (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
      <circle cx="8" cy="8" r="7" stroke="currentColor" strokeWidth="1.5"/>
      <path d="M5.5 5.5l5 5M10.5 5.5l-5 5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
    </svg>
  ),
  warning: (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
      <path d="M8 2L14.5 13.5H1.5L8 2z" stroke="currentColor" strokeWidth="1.5" strokeLinejoin="round"/>
      <path d="M8 6v3.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
      <circle cx="8" cy="11.5" r="0.75" fill="currentColor"/>
    </svg>
  ),
  info: (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
      <circle cx="8" cy="8" r="7" stroke="currentColor" strokeWidth="1.5"/>
      <path d="M8 7v4.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
      <circle cx="8" cy="4.75" r="0.75" fill="currentColor"/>
    </svg>
  ),
  default: (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
      <rect x="1.5" y="1.5" width="13" height="13" rx="3" stroke="currentColor" strokeWidth="1.5"/>
      <path d="M5 8h6M8 5v6" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
    </svg>
  ),
};

// ─── Single Toast Item ────────────────────────────────────────────────────────
const colors: Record<ToastType, { bg: string; border: string; icon: string; bar: string }> = {
  success: { bg: "#0d1f14", border: "#1a4028", icon: "#34d27a", bar: "#34d27a" },
  error:   { bg: "#1f0d0d", border: "#401a1a", icon: "#f87171", bar: "#f87171" },
  warning: { bg: "#1f170d", border: "#40300a", icon: "#fbbf24", bar: "#fbbf24" },
  info:    { bg: "#0d1520", border: "#1a2f45", icon: "#60a5fa", bar: "#60a5fa" },
  default: { bg: "#131318", border: "#2a2a35", icon: "#a0a0b8", bar: "#6366f1" },
};

function ToastItem({ toast: t }: { toast: ToastData }) {
  const [visible, setVisible] = useState(false);
  const [leaving, setLeaving] = useState(false);

  const leave = useCallback(() => {
    setLeaving(true);
    setTimeout(() => dismiss(t.id), 320);
  }, [t.id]);

  useEffect(() => {
    const frame = requestAnimationFrame(() => setVisible(true));
    const timer = setTimeout(leave, t.duration);
    return () => { cancelAnimationFrame(frame); clearTimeout(timer); };
  }, [leave, t.duration]);

  const c = colors[t.type] ?? colors.default;

  return (
    <div style={{
      display: "flex", flexDirection: "column",
      background: c.bg, border: `1px solid ${c.border}`,
      borderRadius: "10px", overflow: "hidden",
      maxWidth: "360px", width: "100%",
      boxShadow: "0 8px 32px rgba(0,0,0,0.5)",
      opacity: visible && !leaving ? 1 : 0,
      transform: visible && !leaving ? "translateY(0) scale(1)" : "translateY(12px) scale(0.97)",
      transition: "opacity 0.28s cubic-bezier(.4,0,.2,1), transform 0.28s cubic-bezier(.4,0,.2,1)",
    }}>
      <div style={{ display: "flex", alignItems: "center", gap: "10px", padding: "12px 14px" }}>
        <span style={{ color: c.icon, flexShrink: 0 }}>{icons[t.type]}</span>
        <span style={{
          flex: 1, fontSize: "13.5px",
          fontFamily: "'DM Sans', 'Segoe UI', sans-serif",
          color: "#e8e8f0", lineHeight: 1.45, letterSpacing: "0.01em",
        }}>
          {t.message}
        </span>
        <button onClick={leave} style={{
          background: "none", border: "none", cursor: "pointer",
          color: "#666", padding: "2px", flexShrink: 0,
          display: "flex", alignItems: "center", justifyContent: "center",
          borderRadius: "4px", transition: "color 0.15s",
        }}
          onMouseEnter={(e) => (e.currentTarget.style.color = "#ccc")}
          onMouseLeave={(e) => (e.currentTarget.style.color = "#666")}
        >
          <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
            <path d="M3 3l8 8M11 3l-8 8" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
          </svg>
        </button>
      </div>
      <div style={{ height: "2px", background: c.border }}>
        <div style={{
          height: "100%", background: c.bar, width: "100%",
          transformOrigin: "left",
          animation: `shrink ${t.duration}ms linear forwards`,
        }}/>
      </div>
      <style>{`@keyframes shrink { from { transform: scaleX(1) } to { transform: scaleX(0) } }`}</style>
    </div>
  );
}

// ─── Toaster ──────────────────────────────────────────────────────────────────
const positions: Record<Position, CSSProperties> = {
  "top-right":     { top: 20, right: 20, bottom: "auto", left: "auto" },
  "top-left":      { top: 20, left: 20,  bottom: "auto", right: "auto" },
  "top-center":    { top: 20, left: "50%", transform: "translateX(-50%)", bottom: "auto" },
  "bottom-right":  { bottom: 20, right: 20, top: "auto", left: "auto" },
  "bottom-left":   { bottom: 20, left: 20,  top: "auto", right: "auto" },
  "bottom-center": { bottom: 20, left: "50%", transform: "translateX(-50%)", top: "auto" },
};

export function Toaster({ position = "bottom-right" }: { position?: Position }) {
  const items = useToasts();
  return (
    <div style={{
      position: "fixed", zIndex: 9999,
      display: "flex", flexDirection: "column",
      gap: "8px", pointerEvents: "none",
      ...(positions[position] ?? positions["bottom-right"]),
    }}>
      {items.map((t) => (
        <div key={t.id} style={{ pointerEvents: "all" }}>
          <ToastItem toast={t} />
        </div>
      ))}
    </div>
  );
}