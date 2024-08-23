DO $$
BEGIN
   IF NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'motto') THEN
      CREATE DATABASE motto;
   END IF;
END
$$;
